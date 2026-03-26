import { Navigate, useRoutes } from "react-router";
import Dashboard from "../pages/dashboard/DashboardPage";
import Layout from "../components/layout/Layout";
import SignInLayout from "../components/layout/SignInLayout";
import useAuth from "../context/useAuth/useAuth";
import UserPage from "../pages/users/UserPage";
import UsersPage from "../pages/users/UsersPage";
import ExpensesPage from "../pages/expenses/ExpensesPage";
import ExpensePage from "../pages/expenses/ExpensePage";
import SavingsPage from "../pages/savings/SavingsPage";
import SavingPage from "../pages/savings/SavingPage";
import UserSettingsPage from "../pages/users/settings/UserSettingsPage";

export default function AppRouter() {
  const { user } = useAuth();

  const authRoutes = [
    { path: "/", element: <Dashboard /> },

    { path: "/savings", element: <SavingsPage /> },
    { path: "/savings/:savingId", element: <SavingPage /> },
    { path: "/savings/:savingId/edit", element: <SavingPage /> },
    { path: "/savings/new", element: <SavingPage /> },

    { path: "/expenses", element: <ExpensesPage /> },
    { path: "/expenses/:expenseId", element: <ExpensePage /> },
    { path: "/expenses/:expenseId/edit", element: <ExpensePage /> },
    { path: "/expenses/new", element: <ExpensePage /> },

    { path: "/settings", element: <UserSettingsPage /> },
    { path: "/settings/edit", element: <UserSettingsPage /> }
  ];

  const adminRoutes = [
    { path: "/users", element: <UsersPage /> },
    { path: "/users/:userId", element: <UserPage /> },
    { path: "/users/:userId/edit", element: <UserPage /> },
    { path: "/users/new", element: <UserPage /> }
  ];

  let Routes;
  if (user) {
    const children = [...authRoutes];

    if (user.role.name.toLowerCase() === "admin") {
      children.push(...adminRoutes);
    }

    children.push({ path: "*", element: <Navigate to="/" replace /> });

    Routes = [
      {
        element: <Layout />,
        children
      }
    ];
  } else {
    Routes = [
      {
        element: <SignInLayout />,
        children: [
          { path: "/", element: <SignInLayout /> },
          { path: "*", element: <Navigate to="/" replace /> }
        ]
      }
    ];
  }

  return useRoutes(Routes);
}
