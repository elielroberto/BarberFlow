import api from "./api";

export const getUsers = async () => {
  return await api.get("/users");
};

export const updateUserRole = async (userId, role) => {
  return await api.put("/users/role", {
    userId,
    role: Number(role),
  });
};