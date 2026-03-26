import { GridColDef } from "@mui/x-data-grid";
import { IUserTable, UserKeys } from "../../models/User";

export default function UserColumns(): GridColDef<IUserTable>[] {
  return [
    {
      field: UserKeys.id,
      headerName: "#",
      width: 250
    },
    {
      field: UserKeys.userName,
      headerName: "Nome de Utilizador",
      flex: 1,
      minWidth: 250
    },
    {
      field: UserKeys.fullName,
      headerName: "Nome",
      flex: 1,
      width: 250
    },
    {
      field: UserKeys.role,
      headerName: "Cargo",
      flex: 1,
      minWidth: 100
    }
  ];
}
