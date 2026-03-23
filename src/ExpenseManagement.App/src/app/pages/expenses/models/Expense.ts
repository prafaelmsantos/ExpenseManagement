export interface IExpense {
  id: string | null;
  amount: number;
  category: string;
  date: Date;
}

export interface IExpenseTable {
  id: string;
  amount: number;
  category: string;
  date: string;
}

export enum ExpenseKeys {
  id = "id",
  amount = "amount",
  category = "category",
  date = "date"
}
