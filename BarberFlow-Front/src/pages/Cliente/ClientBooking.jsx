import { useEffect, useState } from "react";
import { getServices } from "../../services/service.service";
import { getProfessionals } from "../../services/professional.service";
import {
  getAvailableSlots,
  createAppointment, getMyAppointments
} from "../../services/appointment.service";

export default function ClientBooking() {

  const [step, setStep] = useState(1);
  const [professionals, setProfessionals] = useState([]);
  const [selectedProfessional, setSelectedProfessional] = useState(null);
  const [services, setServices] = useState([]);
const [slots, setSlots] = useState([]);

const [selectedSlot, setSelectedSlot] = useState(null);

const [date, setDate] = useState(
  new Date().toISOString().split("T")[0]
);
  const [selectedService, setSelectedService] = useState(null);

  const loadServices = async () => {
    try {

      const response = await getServices();

      setServices(response.data);

    } catch (error) {
      console.log(error);

      alert("Erro ao carregar serviços");
    }
  };

  const loadProfessionals = async () => {
  try {

    const response = await getProfessionals();

    setProfessionals(response.data);

  } catch (error) {

    console.log(error);

    alert("Erro ao carregar profissionais");
  }
};
const loadSlots = async (professionalId) => {
  try {

    const response = await getAvailableSlots(
      professionalId,
      date
    );

    // só livres
    const availableOnly = response.data.filter(
      x => x.available
    );

    setSlots(availableOnly);

  } catch (error) {

    console.log(error);

    alert("Erro ao carregar horários");
  }
};
  useEffect(() => {
    loadServices();
  }, []);

  return (
    <div className="min-h-screen bg-black text-white p-8">

      <div className="max-w-6xl mx-auto">

        {/* HEADER */}
        <h1 className="text-5xl font-bold mb-3 text-center">
          Agendar{" "}
          <span className="text-yellow-500">
            Atendimento
          </span>
        </h1>
        <a
        href="/my-appointments"
        className="text-yellow-500 hover:underline"
      >
        Meus Agendamentos
      </a>
        <p className="text-zinc-400 text-center mb-10">
          Escolha seu serviço e horário
        </p>

        {/* STEPS */}
        <div className="flex justify-center gap-4 mb-12">

          <div className={`px-5 py-2 rounded-full font-bold ${
            step >= 1
              ? "bg-yellow-500 text-black"
              : "bg-zinc-800"
          }`}>
            1. Serviço
          </div>

          <div className={`px-5 py-2 rounded-full font-bold ${
            step >= 2
              ? "bg-yellow-500 text-black"
              : "bg-zinc-800"
          }`}>
            2. Profissional
          </div>

          <div className={`px-5 py-2 rounded-full font-bold ${
            step >= 3
              ? "bg-yellow-500 text-black"
              : "bg-zinc-800"
          }`}>
            3. Horário
          </div>

        </div>

        {/* STEP 1 */}
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

                  await loadProfessionals();

                  setStep(2);

                }}

                  className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6 hover:border-yellow-500 cursor-pointer transition-all duration-200 hover:scale-[1.02]"
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

        {/* STEP 2 */}
{step === 2 && (

  <>
    <div className="flex items-center justify-between mb-8">

      <h2 className="text-3xl font-bold">
        Escolha o Profissional
      </h2>

      <button
        onClick={() => setStep(1)}
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

        await loadSlots(professional.id);

        setStep(3);

      }}

          className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6 hover:border-yellow-500 cursor-pointer transition-all duration-200 hover:scale-[1.02]"
        >

          <h3 className="text-2xl font-bold text-yellow-500">
            {professional.name}
          </h3>

        </div>

      ))}

    </div>
  </>

)}

{/* STEP 3 */}
{step === 3 && (

  <>
    <div className="flex items-center justify-between mb-8">

      <h2 className="text-3xl font-bold">
        Escolha o Horário
      </h2>

      <button
        onClick={() => setStep(2)}
        className="text-yellow-500 hover:underline"
      >
        ← Voltar
      </button>

    </div>

    {/* DATA */}
    <div className="mb-8">

      <input
        type="date"
        value={date}
        onChange={async (e) => {

          setDate(e.target.value);

          await loadSlots(
            selectedProfessional.id
          );

        }}
        className="bg-zinc-800 border border-zinc-700 rounded-lg px-4 py-3 text-white"
      />

    </div>

    {/* SLOTS */}
    <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-5">

      {slots.map((slot, index) => (

        <div
          key={index}

          onClick={() => {
            setSelectedSlot(slot);
          }}

          className={`p-6 rounded-2xl text-center font-bold transition-all duration-200 shadow-lg cursor-pointer ${
            selectedSlot?.start === slot.start
              ? "bg-yellow-500 text-black"
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

        </div>

      ))}

    </div>

    {/* CONFIRMAR */}
    {selectedSlot && (

      <button
        onClick={async () => {

          try {

            await createAppointment({
              professionalId: selectedProfessional.id,
              serviceId: selectedService.id,
              startTime: selectedSlot.start
            });

            alert("Agendamento realizado!");

            window.location.reload();

          } catch (error) {

            console.log(error);

            alert("Erro ao agendar");
          }

        }}

        className="mt-10 w-full bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-4 rounded-2xl text-xl"
      >
        Confirmar Agendamento
      </button>

    )}

  </>

)}

      </div>

    </div>
  );
}