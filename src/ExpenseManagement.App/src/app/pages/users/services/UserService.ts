import { BASE_API_URL } from "../../../config/Variables";
import { IBaseResponse } from "../../../models/BaseResponse";
import {
  getData,
  postData,
  postDeleteData,
  putData
} from "../../../services/BaseService";
import { IUser, IUserTable as IUserTable } from "../models/User";

const getUsers = async (): Promise<IUserTable[]> =>
  await getData<IUserTable[]>(`${BASE_API_URL}/users`);

const getUserSettings = async (): Promise<IUser> =>
  await getData<IUser>(`${BASE_API_URL}/users/settings`);

const getUser = async (id: string): Promise<IUser> =>
  await getData<IUser>(`${BASE_API_URL}/users/${id}`);

const createUser = async (user: IUser): Promise<IUser> =>
  await postData(`${BASE_API_URL}/users`, user);

const updateUser = async (user: IUser): Promise<IUser> =>
  await putData(`${BASE_API_URL}/users/${user.id}`, user);

const deleteUsers = async (ids: string[]): Promise<IBaseResponse[]> =>
  await postDeleteData(`${BASE_API_URL}/users/delete`, ids);

export {
  getUsers,
  getUserSettings,
  getUser,
  createUser,
  updateUser,
  deleteUsers
};
