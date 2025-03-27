using System.Collections.Frozen;
using AutoMapper;
using BetterCinema.Api.Contracts;
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
        public Task<TheaterEntity> GetTheater(int theaterId);
    }

    public class TheatersHandler(
        CinemaDbContext context,
        IMapper mapper,
        IClaimsProvider claimsProvider,
        IFilePersistenceService filePersistenceService)
        : ITheatersHandler
    {
        public async Task<TheaterEntity> GetTheater(int theaterId)
        {
            return await context.Theaters.FirstOrDefaultAsync(t => t.Id == theaterId);
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

            var fileNames = theaters.Where(x => !string.IsNullOrWhiteSpace(x.FileName))
                .Select(x => x.FileName!)
                .Distinct()
                .ToList();

            var filesUrlsTasks = fileNames.Select(GetUrls);
            var fileUrls = await Task.WhenAll(filesUrlsTasks);

            var fileUrlsMap = fileUrls.Where(x => !string.IsNullOrWhiteSpace(x.FileName))
                .ToFrozenDictionary(x => x.FileName, x => x.Url!);

            var theaterModels = theaters.Select(x => MapTheaterResponse(x, fileUrlsMap))
                .ToList();
            return new GetTheatersResponse { Theaters = theaterModels, TotalCount = totalCount };
        }

        private GetTheaterResponse MapTheaterResponse(TheaterEntity entity, FrozenDictionary<string, string> fileUrlsMap)
        {
            var model = mapper.Map<GetTheaterResponse>(entity);
            if (entity.FileName != null)
            {
                model.ImageUrl = fileUrlsMap.GetValueOrDefault(entity.FileName);
            }

            return model;
        }

        private async Task<(string FileName, string? Url)> GetUrls(string fileName)
        {
            return (fileName, await filePersistenceService.GetSignedUrlAsync(fileName));
        }
    }
}