import { z } from "zod";

export const signInSchema = z.object({
  userName: z.string().trim().min(1, "Campo obrigatório."),
  password: z.string().trim().min(1, "Campo obrigatório.")
});

export type ISignInSchema = z.infer<typeof signInSchema>;
