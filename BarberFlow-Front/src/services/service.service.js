import api from "./api";

export const createService = async (data) => {
  return await api.post("/services", data);
};

export const getServices = async () => {
  return await api.get("/services");
};