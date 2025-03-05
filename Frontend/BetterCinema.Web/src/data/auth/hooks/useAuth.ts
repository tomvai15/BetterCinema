import { UserPermissionResponseModel } from "@/data/contracts/Roles/Responses/UserPermissionResponseModel";
import rolesService from "@/data/services/roles-service";
import { useState, useEffect } from "react";
import { ResourceType } from "../ResourceType";
import { AccessType } from "../AccessType";

const useAuth = () => {
  const [permissions, setPermissions] = useState<UserPermissionResponseModel[]>([]);

  useEffect(() => {
    rolesService.getPermissions().then((res) => setPermissions(res.permissions))
  }, []);

  function hasAccess(resourceType: ResourceType, accessType: AccessType): boolean {
    console.log(permissions)
    const permission = permissions.find((p) => p.resourceType==resourceType);

    if (!permission) {
        return false;
    }
    console.log(permission)

    const res = permission.accessType & Number(accessType)
    
    return res != 0
  }

  return [hasAccess];
};

export default useAuth;