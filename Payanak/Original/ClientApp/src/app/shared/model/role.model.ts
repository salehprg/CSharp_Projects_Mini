export class RoleModel {
  id: number;
  name: string;
  title: string;
  canEdit: boolean;
  canDelete: boolean;
  permissions: number[] = [];
}
