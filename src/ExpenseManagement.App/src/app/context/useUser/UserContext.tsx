import { createContext } from "react";
import { IUser } from "../../pages/users/models/User";

export interface UserContextType {
  user: IUser | null;
  setUser: (user: IUser) => void;
  clearUser: () => void;
}

const UserContext = createContext<UserContextType | null>(null);

export default UserContext;
