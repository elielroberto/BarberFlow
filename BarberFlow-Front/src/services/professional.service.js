import api from "./api";

export const getMyProfessionalId = async () => {
  return await api.get("/professional/me");
};