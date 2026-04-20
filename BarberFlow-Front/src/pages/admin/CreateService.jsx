import { useState } from "react";
import { createService } from "../../services/service.service";

export default function CreateService() {
  const [name, setName] = useState("");
  const [duration, setDuration] = useState(30);
  const [price, setPrice] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const slotCount = duration / 30;

      await createService({
        name,
        slotCount,
        price: Number(price),
      });

      alert("Serviço criado com sucesso!");

      setName("");
      setDuration(30);
      setPrice("");
    } catch (error) {
      alert("Erro ao criar serviço");
    }
  };

  return (
    <div className="min-h-screen bg-black flex items-center justify-center p-6">

      <div className="bg-zinc-900 p-8 rounded-2xl border border-zinc-700 w-full max-w-md shadow-xl">

        {/* BOTÃO VOLTAR */}
        <button
          onClick={() => window.history.back()}
          className="text-yellow-500 mb-6 hover:underline flex items-center gap-2"
        >
          ← Voltar
        </button>

        <h1 className="text-3xl font-bold mb-6 text-center text-white">
          Criar <span className="text-yellow-500">Serviço</span>
        </h1>

        <form onSubmit={handleSubmit} className="flex flex-col gap-4">

          <input
            type="text"
            placeholder="Nome do serviço"
            className="p-3 rounded bg-zinc-800 text-white outline-none"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          {/* 🔥 SELECT DE DURAÇÃO */}
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

          <input
            type="number"
            placeholder="Preço"
            className="p-3 rounded bg-zinc-800 text-white outline-none"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
          />

          <button
            type="submit"
            className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-3 rounded transition"
          >
            Criar Serviço
          </button>

        </form>
      </div>
    </div>
  );
}