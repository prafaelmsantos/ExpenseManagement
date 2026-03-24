import { GridColDef } from "@mui/x-data-grid";
import { ISavingTable, SavingKeys } from "../../models/Saving";

export default function SavingColumns(): GridColDef<ISavingTable>[] {
  return [
    {
      field: SavingKeys.id,
      headerName: "#",
      width: 250
    },
    {
      field: SavingKeys.name,
      headerName: "Nome",
      flex: 1,
      width: 250
    },
    {
      field: SavingKeys.category,
      headerName: "Categoria",
      flex: 1,
      width: 250
    },
    {
      field: SavingKeys.amount,
      headerName: "Quantia",
      flex: 1,
      minWidth: 100
    },
    {
      field: SavingKeys.date,
      headerName: "Data",
      flex: 1,
      minWidth: 100
    }
  ];
}
