import { GridColDef } from "@mui/x-data-grid";
import { ExpenseKeys, IExpenseTable } from "../../models/Expense";

export default function ExpenseColumns(): GridColDef<IExpenseTable>[] {
  return [
    {
      field: ExpenseKeys.id,
      headerName: "#",
      width: 250
    },
    {
      field: ExpenseKeys.category,
      headerName: "Categoria",
      flex: 1,
      width: 250
    },
    {
      field: ExpenseKeys.amount,
      headerName: "Quantia",
      flex: 1,
      minWidth: 250
    },
    {
      field: ExpenseKeys.date,
      headerName: "Data",
      flex: 1,
      minWidth: 100
    }
  ];
}
