import { CategoryEnum } from "../../../models/Category";

export interface ISaving {
  id: string | null;
  name: string;
  amount: number;
  category: CategoryEnum;
  date: string;
  description: string | null;
}

export interface ISavingTable {
  id: string | null;
  name: string;
  amount: number;
  category: string;
  date: string;
}

export enum SavingKeys {
  id = "id",
  name = "name",
  amount = "amount",
  category = "category",
  date = "date"
}
