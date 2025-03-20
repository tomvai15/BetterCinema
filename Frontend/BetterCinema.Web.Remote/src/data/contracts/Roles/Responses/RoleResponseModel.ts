import { PermissionResponseModel } from "./PermissionResponseModel";

export type RoleResponseModel = {
    Name: string, 
    Permissions: PermissionResponseModel[], 
}
