import { useContext } from "react";
import AuthContext from "./AuthContext";

export default function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("AuthContext context was used without a provider.");
  }
  return context;
}
