import api from "./api";

export const getAppointmentsByDay = async (professionalId, date) => {
  return await api.get("/schedules/day", {
    params: {
      professionalId,
      date,
    },
  });
};

export const getAvailableSlots = async (professionalId, date) => {
  return await api.get("/schedules/available", {
    params: {
      professionalId,
      date,
    },
  });
};

export const getMySchedule = async () => {
  return await api.get("/schedules/me");
};

export const blockTime = async (data) => {
  return await api.post("/blocked-times", data);
};

export const removeBlock = async (id) => {
  return await api.delete(`/blocked-times/${id}`);
};

export const cancelAppointment = async (id) => {
  return await api.put(`/appointments/${id}/cancel`);
};

export const createAppointment = async (data) => {
  return await api.post("/appointments", data);
};

export const getMyAppointments = async () => {
  return await api.get("/appointments/me");
};