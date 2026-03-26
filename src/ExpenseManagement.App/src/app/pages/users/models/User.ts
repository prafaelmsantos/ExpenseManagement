import { IRole } from "./Role";

export interface IUser {
  id: string | null;
  userName: string;
  firstName: string | null;
  lastName: string | null;
  role: IRole;
}

export interface IUserPassword {
  currentPassword: string;
  newPassword: string;
}

export interface IUserSettings {
  firstName: string | null;
  lastName: string | null;
}

export interface IUserTable {
  id: string | null;
  userName: string;
  fullName: string | null;
  role: string;
}

export enum UserKeys {
  id = "id",
  userName = "userName",
  fullName = "fullName",
  firstName = "firstName",
  lastName = "lastName",
  role = "role"
}
