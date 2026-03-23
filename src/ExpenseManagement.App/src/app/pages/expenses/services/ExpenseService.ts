import { BASE_API_URL } from "../../../config/Variables";
import { IBaseResponse } from "../../../models/BaseResponse";
import {
  getData,
  postData,
  postDeleteData,
  putData
} from "../../../services/BaseService";
import { IExpense, IExpenseTable } from "../models/Expense";

const getExpenses = async (): Promise<IExpenseTable[]> =>
  await getData<IExpenseTable[]>(`${BASE_API_URL}/expenses`);

const getExpense = async (id: string): Promise<IExpense> =>
  await getData<IExpense>(`${BASE_API_URL}/expenses/${id}`);

const createExpense = async (expense: IExpense): Promise<IExpense> =>
  await postData(`${BASE_API_URL}/expenses`, expense);

const updateExpense = async (expense: IExpense): Promise<IExpense> =>
  await putData(`${BASE_API_URL}/expenses/${expense.id}`, expense);

const deleteExpenses = async (ids: string[]): Promise<IBaseResponse[]> =>
  await postDeleteData(`${BASE_API_URL}/expenses/delete`, ids);

export {
  getExpenses,
  getExpense,
  createExpense,
  updateExpense,
  deleteExpenses
};
