import { useContext } from "react";
import UserContext from "./UserContext";

export default function useUser() {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("UserContext deve ser usado dentro do UserProvider");
  }
  return context;
}
