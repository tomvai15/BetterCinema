using System.Collections.Frozen;
using AutoMapper;
using BetterCinema.Api.Contracts.Theaters;
using BetterCinema.Api.Extensions;
using BetterCinema.Api.Providers;
using BetterCinema.Application.FilesManagement.Interfaces;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;
using BetterCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BetterCinema.Api.Handlers
{
    public interface ITheatersHandler
    {
        public Task<GetTheatersResponse> GetTheaters(int limit, int offset);
        public Task<GetTheaterResponse> GetTheater(int theaterId);
    }

    public class TheatersHandler(
        CinemaDbContext context,
        IMapper mapper,
        IClaimsProvider claimsProvider,
        IFilePersistenceService filePersistenceService)
        : ITheatersHandler
    {
        public async Task<GetTheaterResponse> GetTheater(int theaterId)
        {
            var theater = await context.Theaters.FirstOrDefaultAsync(t => t.Id == theaterId);

            var fileUrlsMap = await GetImagesUrlsMapAsync([theater]);

            return MapTheaterResponse(theater, fileUrlsMap);
        }

        public async Task<GetTheatersResponse> GetTheaters(int limit, int offset)
        {
            var totalCount = context.Theaters.Count();

            var theaters = await context.Theaters.GetSetSection(limit, offset).ToListAsync();

            claimsProvider.TryGetUserRole(out var role);
            claimsProvider.TryGetUserId(out var userId);

            if (role != Role.Admin)
            {
                theaters = theaters.Where(t => t.IsConfirmed || t.UserId == userId).ToList();
            }

            var fileUrlsMap = await GetImagesUrlsMapAsync(theaters);

            var theaterModels = theaters.Select(x => MapTheaterResponse(x, fileUrlsMap))
                .ToList();
            return new GetTheatersResponse { Theaters = theaterModels, TotalCount = totalCount };
        }

        private async Task<FrozenDictionary<string, string>> GetImagesUrlsMapAsync(List<TheaterEntity> theaters)
        {
            var fileNames = theaters.Where(x => !string.IsNullOrWhiteSpace(x.FileName))
                .Select(x => x.FileName!)
                .Distinct()
                .ToList();

            var filesUrlsTasks = fileNames.Select(GetUrls);
            var fileUrls = await Task.WhenAll(filesUrlsTasks);

            var fileUrlsMap = fileUrls.Where(x => !string.IsNullOrWhiteSpace(x.FileName))
                .ToFrozenDictionary(x => x.FileName, x => x.Url!);
            return fileUrlsMap;
        }

        private GetTheaterResponse MapTheaterResponse(TheaterEntity entity,
            FrozenDictionary<string, string> fileUrlsMap)
        {
            var model = mapper.Map<GetTheaterResponse>(entity);
            if (entity.FileName != null)
            {
                model.ImageUrl = fileUrlsMap.GetValueOrDefault(entity.FileName);
            }
            else
            {
                model.ImageUrl = null;
            }
            
            return model;
        }

        private async Task<(string FileName, string? Url)> GetUrls(string fileName)
        {
            return (fileName, await filePersistenceService.GetSignedUrlAsync(fileName));
        }
    }
}