import { PermissionAccessResponseModel } from "./PermissionAccessResponseModel";

export type PermissionResponseModel = {
    ResourceName: string, 
    ResourceType: number, 
    Accesses: PermissionAccessResponseModel[], 
}
