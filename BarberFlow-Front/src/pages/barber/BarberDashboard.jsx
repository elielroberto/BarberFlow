import { useEffect, useState } from "react";
import { getAppointmentsByDay } from "../../services/appointment.service";
import { getUser } from "../../utils/auth";

export default function BarberDashboard() {
  const user = getUser();

  const [date, setDate] = useState(
    new Date().toISOString().split("T")[0]
  );

  const [appointments, setAppointments] = useState([]);

  const loadAppointments = async () => {
    try {
      const response = await getAppointmentsByDay(
        user.userId,
        date
      );

      setAppointments(response.data);
    } catch {
      alert("Erro ao carregar agenda");
    }
  };

  useEffect(() => {
    loadAppointments();
  }, [date]);

  return (
    <div className="min-h-screen bg-black text-white p-8">

      <h1 className="text-3xl font-bold mb-6">
        Agenda do <span className="text-yellow-500">Barbeiro</span>
      </h1>

      {/* DATA */}
      <input
        type="date"
        value={date}
        onChange={(e) => setDate(e.target.value)}
        className="mb-6 p-2 rounded bg-zinc-800"
      />

      {/* LISTA */}
      <div className="grid gap-3">

        {appointments.map((a) => (
          <div
            key={a.id}
            className="bg-zinc-900 p-4 rounded border border-zinc-700"
          >
            <p>
              {new Date(a.startTime).toLocaleTimeString([], {
                hour: "2-digit",
                minute: "2-digit",
              })}
              {" - "}
              {new Date(a.endTime).toLocaleTimeString([], {
                hour: "2-digit",
                minute: "2-digit",
              })}
            </p>

            <p className="text-zinc-400 text-sm">
              Cliente: {a.clientName || "—"}
            </p>
          </div>
        ))}

        {appointments.length === 0 && (
          <p className="text-zinc-500">Nenhum agendamento</p>
        )}

      </div>
    </div>
  );
}