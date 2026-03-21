import {
  Dashboard as DashboardIcon,
  People as PeopleIcon
} from "@mui/icons-material";
import { SvgIconComponent } from "@mui/icons-material";

export enum NavType {
  Home = "Início",
  Users = "Utilizadores"
}

export interface Nav {
  name: NavType;
  icon: SvgIconComponent;
  href: string;
}

export const NavItems: Nav[] = [
  { name: NavType.Home, icon: DashboardIcon, href: "/" },
  { name: NavType.Users, icon: PeopleIcon, href: "/users" }
];
