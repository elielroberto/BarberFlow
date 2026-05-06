import { useEffect, useState } from "react";
import { getServices } from "../../services/service.service";
import { getProfessionals } from "../../services/professional.service";
import {
  getAvailableSlots,
  createAppointment,
} from "../../services/appointment.service";

export default function ClientBooking() {
  const [step, setStep] = useState(1);

  const [professionals, setProfessionals] = useState([]);
  const [selectedProfessional, setSelectedProfessional] = useState(null);

  const [services, setServices] = useState([]);
  const [selectedService, setSelectedService] = useState(null);

  const [slots, setSlots] = useState([]);
  const [selectedSlot, setSelectedSlot] = useState(null);

  const [date, setDate] = useState(
    new Date().toISOString().split("T")[0]
  );

  const loadServices = async () => {
    try {
      const response = await getServices();
      setServices(response.data);
    } catch (error) {
      console.log("Erro ao carregar serviços:", error);
      alert("Erro ao carregar serviços");
    }
  };

  const loadProfessionals = async () => {
    try {
      const response = await getProfessionals();
      setProfessionals(response.data);
    } catch (error) {
      console.log("Erro ao carregar profissionais:", error);
      alert("Erro ao carregar profissionais");
    }
  };

  const loadSlots = async (professionalId, selectedDate = date) => {
    try {
      const response = await getAvailableSlots(
        professionalId,
        selectedDate
      );

      const availableOnly = response.data.filter(
        (x) => x.available
      );

      setSlots(availableOnly);
      setSelectedSlot(null);
    } catch (error) {
      console.log("Erro ao carregar horários:", error);
      alert("Erro ao carregar horários");
    }
  };

  const handleConfirmAppointment = async () => {
    try {
      if (!selectedProfessional || !selectedService || !selectedSlot) {
        alert("Selecione serviço, profissional e horário.");
        return;
      }

      const payload = {
        professionalId: selectedProfessional.id,
        serviceId: selectedService.id,
        startTime: selectedSlot.start,
      };

      console.log("Payload agendamento:", payload);

      await createAppointment(payload);

      alert("Agendamento realizado!");

      window.location.href = "/my-appointments";
    } catch (error) {
      console.log("Erro ao agendar:", error);
      console.log("Resposta API:", error.response?.data);

      alert(error.response?.data || "Erro ao agendar");
    }
  };

  useEffect(() => {
    loadServices();
  }, []);

  return (
    <div className="min-h-screen bg-black text-white p-8 pb-40">
      <div className="max-w-6xl mx-auto">
        <h1 className="text-5xl font-bold mb-3 text-center">
          Agendar{" "}
          <span className="text-yellow-500">
            Atendimento
          </span>
        </h1>

        <div className="text-center mb-6">
          <a
            href="/my-appointments"
            className="text-yellow-500 hover:underline"
          >
            Meus Agendamentos
          </a>
        </div>

        <p className="text-zinc-400 text-center mb-10">
          Escolha seu serviço, profissional e horário
        </p>

        <div className="flex justify-center gap-4 mb-12">
          <div
            className={`px-5 py-2 rounded-full font-bold ${
              step >= 1
                ? "bg-yellow-500 text-black"
                : "bg-zinc-800"
            }`}
          >
            1. Serviço
          </div>

          <div
            className={`px-5 py-2 rounded-full font-bold ${
              step >= 2
                ? "bg-yellow-500 text-black"
                : "bg-zinc-800"
            }`}
          >
            2. Profissional
          </div>

          <div
            className={`px-5 py-2 rounded-full font-bold ${
              step >= 3
                ? "bg-yellow-500 text-black"
                : "bg-zinc-800"
            }`}
          >
            3. Horário
          </div>
        </div>

        {step === 1 && (
          <>
            <h2 className="text-3xl font-bold mb-8">
              Escolha o Serviço
            </h2>

            <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
              {services.map((service) => (
                <div
                  key={service.id}
                  onClick={async () => {
                    setSelectedService(service);
                    setSelectedProfessional(null);
                    setSelectedSlot(null);
                    setSlots([]);

                    await loadProfessionals();

                    setStep(2);
                  }}
                  className={`bg-zinc-900 border rounded-2xl p-6 cursor-pointer transition-all duration-200 hover:scale-[1.02] ${
                    selectedService?.id === service.id
                      ? "border-yellow-500"
                      : "border-zinc-800 hover:border-yellow-500"
                  }`}
                >
                  <h3 className="text-2xl font-bold mb-4 text-yellow-500">
                    {service.name}
                  </h3>

                  <div className="space-y-2 text-zinc-300">
                    <p>
                      <strong>Preço:</strong>{" "}
                      R$ {Number(service.price).toFixed(2)}
                    </p>

                    <p>
                      <strong>Duração:</strong>{" "}
                      {service.slotCount * 30} min
                    </p>
                  </div>
                </div>
              ))}
            </div>
          </>
        )}

        {step === 2 && (
          <>
            <div className="flex items-center justify-between mb-8">
              <h2 className="text-3xl font-bold">
                Escolha o Profissional
              </h2>

              <button
                onClick={() => {
                  setStep(1);
                  setSelectedProfessional(null);
                  setSelectedSlot(null);
                  setSlots([]);
                }}
                className="text-yellow-500 hover:underline"
              >
                ← Voltar
              </button>
            </div>

            <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
              {professionals.map((professional) => (
                <div
                  key={professional.id}
                  onClick={async () => {
                    setSelectedProfessional(professional);
                    setSelectedSlot(null);

                    await loadSlots(professional.id, date);

                    setStep(3);
                  }}
                  className={`bg-zinc-900 border rounded-2xl p-6 cursor-pointer transition-all duration-200 hover:scale-[1.02] ${
                    selectedProfessional?.id === professional.id
                      ? "border-yellow-500"
                      : "border-zinc-800 hover:border-yellow-500"
                  }`}
                >
                  <h3 className="text-2xl font-bold text-yellow-500">
                    {professional.name}
                  </h3>
                </div>
              ))}
            </div>
          </>
        )}

        {step === 3 && (
          <>
            <div className="flex items-center justify-between mb-8">
              <h2 className="text-3xl font-bold">
                Escolha o Horário
              </h2>

              <button
                onClick={() => {
                  setStep(2);
                  setSelectedSlot(null);
                  setSlots([]);
                }}
                className="text-yellow-500 hover:underline"
              >
                ← Voltar
              </button>
            </div>

            <div className="mb-8">
              <label className="block text-zinc-400 mb-2">
                Data do agendamento
              </label>

              <input
                type="date"
                value={date}
                onChange={async (e) => {
                  const newDate = e.target.value;

                  setDate(newDate);
                  setSelectedSlot(null);

                  if (selectedProfessional) {
                    await loadSlots(selectedProfessional.id, newDate);
                  }
                }}
                className="bg-zinc-800 border border-zinc-700 rounded-lg px-4 py-3 text-white"
              />
            </div>

            {slots.length === 0 && (
              <div className="bg-zinc-900 border border-zinc-800 rounded-2xl p-8 text-center">
                <p className="text-zinc-400">
                  Nenhum horário disponível para esta data.
                </p>
              </div>
            )}

            {slots.length > 0 && (
              <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-5">
                {slots.map((slot, index) => (
                  <button
                    type="button"
                    key={`${slot.start}-${index}`}
                    onClick={() => {
                      console.log("Slot clicado:", slot);
                      setSelectedSlot(slot);
                    }}
                    className={`p-6 rounded-2xl text-center font-bold transition-all duration-200 shadow-lg cursor-pointer ${
                      selectedSlot?.start === slot.start
                        ? "bg-yellow-500 text-black scale-[1.03]"
                        : "bg-green-600 hover:bg-green-500"
                    }`}
                  >
                    <p className="text-3xl mb-2">
                      {new Date(slot.start).toLocaleTimeString([], {
                        hour: "2-digit",
                        minute: "2-digit",
                      })}
                    </p>

                    <span className="text-sm">
                      Disponível
                    </span>
                  </button>
                ))}
              </div>
            )}

            {selectedSlot && (
              <div className="fixed bottom-0 left-0 right-0 bg-zinc-950 border-t border-zinc-800 p-4 z-50">
                <div className="max-w-6xl mx-auto">
                  <div className="mb-3 text-center">
                    <p className="text-zinc-400 text-sm">
                      Horário selecionado
                    </p>

                    <p className="text-2xl font-bold text-yellow-500">
                      {new Date(selectedSlot.start).toLocaleTimeString([], {
                        hour: "2-digit",
                        minute: "2-digit",
                      })}
                    </p>
                  </div>

                  <button
                    type="button"
                    onClick={handleConfirmAppointment}
                    className="w-full bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-4 rounded-2xl text-xl"
                  >
                    Confirmar Agendamento
                  </button>
                </div>
              </div>
            )}
          </>
        )}
      </div>
    </div>
  );
}