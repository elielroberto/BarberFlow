import { useEffect, useState } from "react";
import { getMyProfessionalId } from "../../services/professional.service";

import {
  getAvailableSlots,
  blockTime,
  cancelAppointment,
  removeBlock
} from "../../services/appointment.service";

export default function BarberDashboard() {

  const [slots, setSlots] = useState([]);
  const [selectedSlot, setSelectedSlot] = useState(null);
  const [selectedFreeSlot, setSelectedFreeSlot] = useState(null);
  const [reason, setReason] = useState("");

  const [date, setDate] = useState(
    new Date().toISOString().split("T")[0]
  );

  const loadAppointments = async () => {
    try {

      const prof = await getMyProfessionalId();

      const response = await getAvailableSlots(
        prof.data,
        date
      );

      setSlots(response.data);

    } catch (error) {
      console.log(error);
      alert("Erro ao carregar agenda");
    }
  };

  const handleBlockTime = async () => {
    try {

      const startDate = new Date(selectedFreeSlot.start);

      const endDate = new Date(startDate);

      endDate.setMinutes(endDate.getMinutes() + 30);

      const start =
        `${startDate.getFullYear()}-${String(startDate.getMonth() + 1).padStart(2, "0")}-${String(startDate.getDate()).padStart(2, "0")}T${String(startDate.getHours()).padStart(2, "0")}:${String(startDate.getMinutes()).padStart(2, "0")}:00`;

      const end =
        `${endDate.getFullYear()}-${String(endDate.getMonth() + 1).padStart(2, "0")}-${String(endDate.getDate()).padStart(2, "0")}T${String(endDate.getHours()).padStart(2, "0")}:${String(endDate.getMinutes()).padStart(2, "0")}:00`;

      await blockTime({
        start,
        end,
        reason
      });

      alert("Horário bloqueado!");

      setSelectedFreeSlot(null);
      setReason("");

      loadAppointments();

    } catch (error) {
      console.log(error);
      alert("Erro ao bloquear horário");
    }
  };

  const handleCancelAppointment = async () => {
    try {

      await cancelAppointment(selectedSlot.appointmentId);

      alert("Agendamento cancelado!");

      setSelectedSlot(null);

      loadAppointments();

    } catch (error) {
      console.log(error);
      alert("Erro ao cancelar");
    }
  };

  const handleRemoveBlock = async () => {
    try {

      await removeBlock(selectedSlot.blockedId);

      alert("Bloqueio removido!");

      setSelectedSlot(null);

      loadAppointments();

    } catch (error) {
      console.log(error);
      alert("Erro ao remover bloqueio");
    }
  };

  useEffect(() => {
    loadAppointments();
  }, [date]);

  return (
    <div className="min-h-screen bg-black text-white p-8">

      <div className="max-w-6xl mx-auto">

        {/* HEADER */}
        <h1 className="text-5xl font-bold mb-4 text-center">
          Agenda do{" "}
          <span className="text-yellow-500">
            Barbeiro
          </span>
        </h1>

        <p className="text-zinc-400 text-center mb-8 text-lg">
          Gerencie seus horários e acompanhe seus atendimentos
        </p>

        {/* DATA */}
        <div className="flex justify-center mb-10">

          <input
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
            className="bg-zinc-800 border border-zinc-700 rounded-lg px-4 py-3 text-white"
          />

        </div>

        {/* LEGENDA */}
        <div className="flex gap-6 mb-8">

          <div className="flex items-center gap-2">
            <div className="w-5 h-5 bg-green-600 rounded"></div>
            <span>Livre</span>
          </div>

          <div className="flex items-center gap-2">
            <div className="w-5 h-5 bg-red-600 rounded"></div>
            <span>Ocupado</span>
          </div>

          <div className="flex items-center gap-2">
            <div className="w-5 h-5 bg-zinc-700 rounded"></div>
            <span>Bloqueado</span>
          </div>

        </div>

        {/* GRID */}
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-5">

          {slots.map((slot, index) => (

            <div
              key={index}

              onClick={() => {

                if (slot.available) {

                  setSelectedFreeSlot(slot);

                } else {

                  setSelectedSlot(slot);

                }

              }}

              className={`p-6 rounded-2xl text-center font-bold transition-all duration-200 shadow-lg cursor-pointer ${
                slot.available
                  ? "bg-green-600 hover:bg-green-500"
                  : slot.isBlocked
                    ? "bg-zinc-700 hover:bg-zinc-600"
                    : "bg-red-700 hover:bg-red-600"
              }`}
            >

              <p className="text-3xl mb-2">
                {new Date(slot.start).toLocaleTimeString([], {
                  hour: "2-digit",
                  minute: "2-digit",
                })}
              </p>

              <span className="text-sm">
                {slot.available
                  ? "Disponível"
                  : slot.isBlocked
                    ? "Bloqueado"
                    : "Ocupado"}
              </span>

            </div>

          ))}

        </div>

        {/* EMPTY */}
        {slots.length === 0 && (
          <div className="text-center mt-10 text-zinc-500">
            Nenhum horário encontrado
          </div>
        )}

      </div>

      {/* MODAL BLOQUEAR */}
      {selectedFreeSlot && (

        <div className="fixed inset-0 bg-black/80 flex items-center justify-center z-50">

          <div className="bg-zinc-900 border border-zinc-700 rounded-2xl p-8 w-full max-w-md">

            <h2 className="text-3xl font-bold mb-6 text-yellow-500">
              Bloquear Horário
            </h2>

            <div className="space-y-4">

              <p>
                <strong>Horário:</strong>{" "}
                {new Date(selectedFreeSlot.start).toLocaleTimeString([], {
                  hour: "2-digit",
                  minute: "2-digit",
                })}
              </p>

              <textarea
                placeholder="Motivo do bloqueio"
                value={reason}
                onChange={(e) => setReason(e.target.value)}
                className="w-full bg-zinc-800 border border-zinc-700 rounded-xl p-4 text-white"
              />

            </div>

            <div className="flex gap-3 mt-6">

              <button
                onClick={() => {
                  setSelectedFreeSlot(null);
                  setReason("");
                }}
                className="flex-1 bg-zinc-700 hover:bg-zinc-600 py-3 rounded-xl"
              >
                Cancelar
              </button>

              <button
                onClick={handleBlockTime}
                className="flex-1 bg-red-600 hover:bg-red-500 py-3 rounded-xl font-bold"
              >
                Bloquear
              </button>

            </div>

          </div>

        </div>

      )}

      {/* MODAL OCUPADO / BLOQUEADO */}
      {selectedSlot && (

        <div className="fixed inset-0 bg-black/80 flex items-center justify-center z-50">

          <div className="bg-zinc-900 border border-zinc-700 rounded-2xl p-8 w-full max-w-md">

            <h2 className="text-3xl font-bold mb-6 text-yellow-500">

              {selectedSlot.isBlocked
                ? "Horário Bloqueado"
                : "Atendimento"}

            </h2>

            <div className="space-y-4 text-lg">

              {selectedSlot.isBlocked ? (

                <>
                  <p>
                    Este horário está bloqueado.
                  </p>

                  <p>
                    <strong>Horário:</strong>{" "}
                    {new Date(selectedSlot.start).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </p>

                  <button
                    onClick={handleRemoveBlock}
                    className="mt-4 w-full bg-zinc-700 hover:bg-zinc-600 py-3 rounded-xl font-bold"
                  >
                    Desbloquear Horário
                  </button>
                </>

              ) : (

                <>
                  <p>
                    <strong>Cliente:</strong>{" "}
                    {selectedSlot.clientName}
                  </p>

                  <p>
                    <strong>Serviço:</strong>{" "}
                    {selectedSlot.serviceName}
                  </p>

                  <p>
                    <strong>Horário:</strong>{" "}
                    {new Date(selectedSlot.start).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </p>

                  <button
                    onClick={handleCancelAppointment}
                    className="mt-4 w-full bg-red-600 hover:bg-red-500 py-3 rounded-xl font-bold"
                  >
                    Cancelar Atendimento
                  </button>
                </>

              )}

            </div>

            <button
              onClick={() => setSelectedSlot(null)}
              className="mt-6 w-full bg-yellow-500 hover:bg-yellow-600 text-black py-3 rounded-xl font-bold"
            >
              Fechar
            </button>

          </div>

        </div>

      )}

    </div>
  );
}