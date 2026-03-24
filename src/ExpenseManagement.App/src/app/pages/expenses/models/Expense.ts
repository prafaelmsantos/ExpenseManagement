export interface IExpense {
  id: string | null;
  amount: number;
  category: ExpenseCategory;
  date: string;
}

export interface IExpenseTable {
  id: string;
  amount: number;
  category: ExpenseCategory;
  date: string;
}

export enum ExpenseCategory {
  Housing = 1,
  Food = 2,
  Transport = 3,
  Car = 4,
  Health = 5,
  Entertainment = 6,
  Education = 7,
  Utilities = 8,
  Insurance = 9,
  Taxes = 10,
  Shopping = 11,
  Travel = 12,
  Investments = 13,
  Subscriptions = 14,
  Gifts = 15,
  Other = 99
}

export const ExpenseCategoryPt: Record<ExpenseCategory, string> = {
  [ExpenseCategory.Housing]: "Habitação",
  [ExpenseCategory.Food]: "Alimentação",
  [ExpenseCategory.Transport]: "Transporte",
  [ExpenseCategory.Car]: "Carro",
  [ExpenseCategory.Health]: "Saúde",
  [ExpenseCategory.Entertainment]: "Entretenimento",
  [ExpenseCategory.Education]: "Educação",
  [ExpenseCategory.Utilities]: "Serviços",
  [ExpenseCategory.Insurance]: "Seguros",
  [ExpenseCategory.Taxes]: "Impostos",
  [ExpenseCategory.Shopping]: "Compras",
  [ExpenseCategory.Travel]: "Viagem",
  [ExpenseCategory.Investments]: "Investimentos",
  [ExpenseCategory.Subscriptions]: "Subscrições",
  [ExpenseCategory.Gifts]: "Presentes",
  [ExpenseCategory.Other]: "Outros"
};

export enum ExpenseKeys {
  id = "id",
  amount = "amount",
  category = "category",
  date = "date"
}
