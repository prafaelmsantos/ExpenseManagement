import { z } from "zod";
import { ExpenseCategory } from "../models/Expense";

export const expenseSchema = z.object({
  id: z.string().nullable(),
  amount: z.number().min(0, "Campo obrigatório."),
  category: z.enum(ExpenseCategory, "Campo obrigatório."),
  date: z.string().trim().min(1, "Campo obrigatório.")
});

export type IExpenseSchema = z.infer<typeof expenseSchema>;
