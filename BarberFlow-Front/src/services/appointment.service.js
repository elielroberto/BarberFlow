import api from "./api";

export const getAppointmentsByDay = async (professionalId, date) => {
  return await api.get("/appointment/day", {
    params: {
      professionalId,
      date,
    },
  });
};

export const getAvailableSlots = async (professionalId, date) => {
  return await api.get("/appointment/available", {
    params: {
      professionalId,
      date,
    },
  });
};

export const blockTime = async (data) => {
  return await api.post("/appointment/block", data);
};

export const cancelAppointment = async (id) => {
  return await api.put(`/appointment/${id}/cancel`);
};

export const removeBlock = async (id) => {
  return await api.delete(`/appointment/block/${id}`);
};