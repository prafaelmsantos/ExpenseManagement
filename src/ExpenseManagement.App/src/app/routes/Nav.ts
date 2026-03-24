import {
  Dashboard as DashboardIcon,
  People as PeopleIcon,
  AccountBalanceWallet as WalletIcon,
  AttachMoney as MoneyIcon
} from "@mui/icons-material";
import { SvgIconComponent } from "@mui/icons-material";

export enum NavType {
  Home = "Início",
  Savings = "Poupanças",
  Expenses = "Despesas",
  Users = "Utilizadores"
}

export interface Nav {
  name: NavType;
  icon: SvgIconComponent;
  href: string;
}

export const NavItems: Nav[] = [
  { name: NavType.Home, icon: DashboardIcon, href: "/" },
  { name: NavType.Savings, icon: WalletIcon, href: "/savings" },
  { name: NavType.Expenses, icon: MoneyIcon, href: "/expenses" },
  { name: NavType.Users, icon: PeopleIcon, href: "/users" }
];
