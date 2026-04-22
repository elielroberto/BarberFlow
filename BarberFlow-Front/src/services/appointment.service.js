import api from "./api";

export const getAppointmentsByDay = async (professionalId, date) => {
  return await api.get("/appointment/day", {
    params: {
      professionalId,
      date,
    },
  });
};