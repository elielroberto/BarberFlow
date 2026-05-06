import { useEffect, useState } from "react";

import {
  getMyAppointments,
  cancelAppointment
} from "../../services/appointment.service";

export default function MyAppointments() {

  const [appointments, setAppointments] = useState([]);

  const loadAppointments = async () => {
    try {

      const response = await getMyAppointments();

      setAppointments(response.data);

    } catch (error) {

      console.log(error);

      alert("Erro ao carregar agendamentos");
    }
  };

  const handleCancel = async (id) => {

    if (!confirm("Deseja cancelar o agendamento?"))
      return;

    try {
     
      await cancelAppointment(id);
      console.log(id);
      alert("Agendamento cancelado!");

      loadAppointments();

    } catch (error) {

      console.log(error);

      alert("Erro ao cancelar");
    }
  };

  useEffect(() => {
    loadAppointments();
  }, []);

  return (
    <div className="min-h-screen bg-black text-white p-8">

      <div className="max-w-5xl mx-auto">

        {/* HEADER */}
        <div className="flex items-center justify-between mb-10">

          <div>
            <h1 className="text-5xl font-bold">
              Meus{" "}
              <span className="text-yellow-500">
                Agendamentos
              </span>
            </h1>

            <p className="text-zinc-400 mt-2">
              Visualize e gerencie seus horários
            </p>
          </div>

          <a
            href="/booking"
            className="bg-yellow-500 hover:bg-yellow-600 text-black px-5 py-3 rounded-xl font-bold"
          >
            Novo Agendamento
          </a>

        </div>

        {/* LISTA */}
        <div className="grid gap-5">

          {appointments.map((appointment) => (

            <div
              key={appointment.id}
              className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6"
            >

              <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-6">

                <div className="space-y-2">

                  <h2 className="text-2xl font-bold text-yellow-500">
                    {appointment.serviceName}
                  </h2>

                  <p className="text-zinc-300">
                    <strong>Profissional:</strong>{" "}
                    {appointment.professionalName}
                  </p>

                  <p className="text-zinc-300">
                    <strong>Data:</strong>{" "}
                    {new Date(appointment.startTime).toLocaleDateString()}
                  </p>

                  <p className="text-zinc-300">
                    <strong>Horário:</strong>{" "}
                    {new Date(appointment.startTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </p>

                </div>

                <div>

                  <button
                    onClick={() => handleCancel(appointment.id)}
                    className="bg-red-600 hover:bg-red-500 px-5 py-3 rounded-xl font-bold"
                  >
                    Cancelar
                  </button>

                </div>

              </div>

            </div>

          ))}

          {/* EMPTY */}
          {appointments.length === 0 && (

            <div className="text-center text-zinc-500 mt-10">
              Nenhum agendamento encontrado
            </div>

          )}

        </div>

      </div>

    </div>
  );
}