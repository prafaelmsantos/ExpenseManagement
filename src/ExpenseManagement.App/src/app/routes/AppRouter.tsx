import { Navigate, useRoutes } from "react-router";
import Dashboard from "../pages/dashboard/DashboardPage";
import Layout from "../components/layout/Layout";
import SignInLayout from "../components/layout/SignInLayout";
import useAuth from "../context/useAuth/useAuth";
import UserPage from "../pages/users/UserPage";
import UsersPage from "../pages/users/UsersPage";
import ExpensesPage from "../pages/expenses/ExpensesPage";
import ExpensePage from "../pages/expenses/ExpensePAge";

export default function AppRouter() {
  const { user } = useAuth();

  const Routes = user
    ? [
        {
          element: <Layout />,
          children: [
            { path: "/", element: <Dashboard /> },

            { path: "/expenses", element: <ExpensesPage /> },
            { path: "/expenses/:expenseId", element: <ExpensePage /> },
            { path: "/expenses/:expenseId/edit", element: <ExpensePage /> },
            { path: "/expenses/new", element: <ExpensePage /> },

            { path: "/users", element: <UsersPage /> },
            { path: "/users/:userId", element: <UserPage /> },
            { path: "/users/:userId/edit", element: <UserPage /> },
            { path: "/users/new", element: <UserPage /> },

            { path: "*", element: <Navigate to="/" replace /> }
          ]
        }
      ]
    : [
        {
          element: <SignInLayout />,
          children: [
            { path: "/", element: <SignInLayout /> },
            { path: "*", element: <Navigate to="/" replace /> }
          ]
        }
      ];

  return useRoutes(Routes);
}
