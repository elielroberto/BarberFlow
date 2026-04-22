import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getServiceById, updateService } from "../../services/service.service";

export default function EditService() {
  const { id } = useParams();

  const [name, setName] = useState("");
  const [duration, setDuration] = useState(30);
  const [price, setPrice] = useState("");

  const loadService = async () => {
    try {
      const response = await getServiceById(id);
      const service = response.data;

      setName(service.name);
      setPrice(service.price);

      setDuration(service.slotCount * 30);

    } catch {
      alert("Erro ao carregar serviço");
    }
  };

  useEffect(() => {
    loadService();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const slotCount = duration / 30;

      await updateService(id, {
        name,
        slotCount,
        price: Number(price),
      });

      alert("Serviço atualizado com sucesso!");

      window.location.href = "/admin/services";

    } catch {
      alert("Erro ao atualizar serviço");
    }
  };

  return (
    <div
      className="min-h-screen bg-black bg-cover bg-center relative"
      style={{ backgroundImage: "url('/barber-bg.png')" }}
    >
      {/* overlay */}
      <div className="absolute inset-0 bg-black/85"></div>

      <div className="relative flex items-center justify-center min-h-screen p-6">

        <div className="bg-zinc-900/70 backdrop-blur p-8 rounded-2xl border border-zinc-700 w-full max-w-md shadow-xl">

          {/* 🔙 VOLTAR */}
          <button
            onClick={() => window.history.back()}
            className="text-yellow-500 mb-6 hover:underline"
          >
            ← Voltar
          </button>

          <h1 className="text-3xl font-bold mb-6 text-center text-white">
            Editar <span className="text-yellow-500">Serviço</span>
          </h1>

          <form onSubmit={handleSubmit} className="flex flex-col gap-4">

            {/* Nome */}
            <input
              type="text"
              placeholder="Nome do serviço"
              className="p-3 rounded bg-zinc-800 text-white outline-none"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />

            {/* Duração */}
            <select
              className="p-3 rounded bg-zinc-800 text-white outline-none"
              value={duration}
              onChange={(e) => setDuration(Number(e.target.value))}
            >
              <option value={30}>30 minutos</option>
              <option value={60}>1 hora</option>
              <option value={90}>1h30</option>
              <option value={120}>2 horas</option>
            </select>

            {/* Preço */}
            <input
              type="number"
              placeholder="Preço"
              className="p-3 rounded bg-zinc-800 text-white outline-none"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
            />

            {/* Botão */}
            <button
              type="submit"
              className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-3 rounded transition"
            >
              Salvar Alterações
            </button>

          </form>

        </div>
      </div>
    </div>
  );
}