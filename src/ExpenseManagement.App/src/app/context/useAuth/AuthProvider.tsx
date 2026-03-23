import React, { useEffect, useState } from "react";
import AuthContext from "./AuthContext.tsx";
import { IUser } from "../../pages/users/models/User.ts";

interface Props {
  children: React.ReactNode;
}

export function AuthProvider({ children }: Props) {
  const [user, setUserState] = useState<IUser | null>(null);

  useEffect(() => {
    const stored = localStorage.getItem("user");
    if (stored) {
      setUserState(JSON.parse(stored));
    }
  }, []);

  const setUser = (user: IUser) => {
    setUserState(user);
    localStorage.setItem("user", JSON.stringify(user));
  };

  const clearUser = () => {
    setUserState(null);
    localStorage.removeItem("token");
    localStorage.removeItem("user");
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        setUser,
        clearUser
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}
