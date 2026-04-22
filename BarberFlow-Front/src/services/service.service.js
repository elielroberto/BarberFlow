import api from "./api";

export const createService = async (data) => {
  return await api.post("/services", data);
};

export const getServices = async () => {
  return await api.get("/services");
};

export const deleteService = async (serviceId) => {
  return await api.delete(`/services/${serviceId}`);  
};

export const getServiceById = async (id) => {
  return await api.get(`/services/${id}`);
};

export const updateService = async (id, data) => {
  return await api.put(`/services/${id}`, data);
};