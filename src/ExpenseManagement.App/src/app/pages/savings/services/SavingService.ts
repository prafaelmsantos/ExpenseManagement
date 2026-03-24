import { BASE_API_URL } from "../../../config/Variables";
import { IBaseResponse } from "../../../models/BaseResponse";
import {
  getData,
  postData,
  postDeleteData,
  putData
} from "../../../services/BaseService";
import { ISaving } from "../models/Saving";

const getSavings = async (): Promise<ISaving[]> =>
  await getData<ISaving[]>(`${BASE_API_URL}/savings`);

const getSaving = async (id: string): Promise<ISaving> =>
  await getData<ISaving>(`${BASE_API_URL}/savings/${id}`);

const createSaving = async (saving: ISaving): Promise<ISaving> =>
  await postData(`${BASE_API_URL}/savings`, saving);

const updateSaving = async (saving: ISaving): Promise<ISaving> =>
  await putData(`${BASE_API_URL}/savings/${saving.id}`, saving);

const deleteSavings = async (ids: string[]): Promise<IBaseResponse[]> =>
  await postDeleteData(`${BASE_API_URL}/savings/delete`, ids);

export { getSavings, getSaving, createSaving, updateSaving, deleteSavings };
