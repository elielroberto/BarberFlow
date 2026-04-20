import { Navigate } from "react-router-dom";
import { getUser } from "../utils/auth";

export default function PrivateRoute({ children, allowedRoles }) {
  const user = getUser();

  //  não logado
  if (!user) {
    return <Navigate to="/" />;
  }

  //  sem permissão
  if (allowedRoles && !allowedRoles.includes(user.role)) {
    return <Navigate to="/unauthorized" />;
  }

  // permitido
  return children;
}