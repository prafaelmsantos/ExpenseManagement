import { z } from "zod";

export const expenseSchema = z.object({
  id: z.string().nullable(),
  amount: z.number().min(0, "Campo obrigatório."),
  category: z.string().trim().min(1, "Campo obrigatório."),
  date: z.date().min(1, "Campo obrigatório.")
});

export type IExpenseSchema = z.infer<typeof expenseSchema>;
