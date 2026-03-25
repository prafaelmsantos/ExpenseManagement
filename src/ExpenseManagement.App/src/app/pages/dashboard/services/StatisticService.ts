import { BASE_API_URL } from "../../../config/Variables";
import { getData, postData } from "../../../services/BaseService";
import { IChart, IYear } from "../models/Statistic";

const getYears = async (): Promise<IYear> =>
  await getData(`${BASE_API_URL}/statistics/years`);

const getExpensesByYears = async (chartRequest: IYear): Promise<IChart> =>
  await postData(`${BASE_API_URL}/statistics/expenses/years`, chartRequest);

const getSavingsByYears = async (chartRequest: IYear): Promise<IChart> =>
  await postData(`${BASE_API_URL}/statistics/savings/years`, chartRequest);

export { getYears, getExpensesByYears, getSavingsByYears };
