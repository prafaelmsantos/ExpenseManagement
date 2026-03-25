import { BASE_API_URL } from "../../../config/Variables";
import { getData, postData } from "../../../services/BaseService";
import { IChart, ISparkLineChart, IYear } from "../models/Statistic";

const getYears = async (): Promise<IYear> =>
  await getData(`${BASE_API_URL}/statistics/years`);

const getExpensesLineChart = async (year: IYear): Promise<IChart[]> =>
  await postData(`${BASE_API_URL}/statistics/expenses/chart/line`, year);

const getSparkLineChart = async (): Promise<ISparkLineChart[]> =>
  await getData(`${BASE_API_URL}/statistics/chart/spark/line`);

const getSavingsLineChart = async (year: IYear): Promise<IChart[]> =>
  await postData(`${BASE_API_URL}/statistics/savings/chart/line`, year);

const getExpensesBarChart = async (year: IYear): Promise<IChart[]> =>
  await postData(`${BASE_API_URL}/statistics/expenses/chart/bar`, year);

const getSavingsBarChart = async (year: IYear): Promise<IChart[]> =>
  await postData(`${BASE_API_URL}/statistics/savings/chart/bar`, year);

export {
  getYears,
  getSparkLineChart,
  getExpensesLineChart,
  getSavingsLineChart,
  getExpensesBarChart,
  getSavingsBarChart
};
