import React, { useEffect, useState } from "react";
import UserContext from "./UserContext.tsx";
import { IUser } from "../../pages/users/models/User.ts";

interface Props {
  children: React.ReactNode;
}

export function UserProvider({ children }: Props) {
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
    localStorage.removeItem("user");
  };

  return (
    <UserContext.Provider
      value={{
        user,
        setUser,
        clearUser
      }}
    >
      {children}
    </UserContext.Provider>
  );
}
