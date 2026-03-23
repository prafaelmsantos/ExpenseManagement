import { createContext } from "react";
import { IUser } from "../../pages/users/models/User";

export interface AuthContextType {
  user: IUser | null;
  setUser: (user: IUser) => void;
  clearUser: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

export default AuthContext;
