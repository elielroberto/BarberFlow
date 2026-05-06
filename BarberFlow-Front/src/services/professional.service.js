import api from "./api";

export const getMyProfessionalId = async () => {
  return await api.get("/professionals/me");
};

export const getProfessionals = async () => {
  return await api.get("/professionals");
};