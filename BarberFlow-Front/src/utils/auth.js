import { jwtDecode } from "jwt-decode";

export const getUser = () => {
  const token = localStorage.getItem("token");

  if (!token) return null;

  try {
    const decoded = jwtDecode(token);

    return {
      ...decoded,
      role:
        decoded.role ||
        decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
    };
  } catch {
    return null;
  }
};