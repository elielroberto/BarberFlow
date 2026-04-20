import { useEffect, useState } from "react";
import { getServices } from "../../services/service.service";

export default function ServicesList() {
  const [services, setServices] = useState([]);
  const [loading, setLoading] = useState(true);

  const loadServices = async () => {
    try {
      const response = await getServices();
      setServices(response.data);
    } catch (error) {
      alert("Erro ao carregar serviços");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadServices();
  }, []);

  return (
    <div className="min-h-screen bg-black text-white p-8">
      <div className="max-w-6xl mx-auto">
        <div className="flex items-center justify-between mb-8">
                    {/* 🔙 BOTÃO VOLTAR */}
        <button
          onClick={() => window.history.back()}
          className="text-yellow-500 mb-6 hover:underline flex items-center gap-2"
        >
          ← Voltar
        </button>
          <h1 className="text-3xl font-bold">
            Serviços <span className="text-yellow-500">Cadastrados</span>
          </h1>

          <a
            href="/admin/services/create"
            className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold px-4 py-2 rounded transition"
          >
            Novo Serviço
          </a>
        </div>

        {loading ? (
          <p className="text-zinc-400">Carregando serviços...</p>
        ) : services.length === 0 ? (
          <div className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6 text-center">
            <p className="text-zinc-400">Nenhum serviço cadastrado.</p>
          </div>
        ) : (
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
            {services.map((service) => (
              <div
                key={service.id}
                className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6 shadow-lg"
              >
                <h2 className="text-xl font-semibold mb-3 text-white">
                  {service.name}
                </h2>

                <div className="space-y-2 text-zinc-300">
                  <p>
                    <span className="text-zinc-500">Slots:</span>{" "}
                    {service.slotCount}
                  </p>

                  <p>
                    <span className="text-zinc-500">Duração total:</span>{" "}
                    {service.slotCount * 30} min
                  </p>

                  <p>
                    <span className="text-zinc-500">Preço:</span>{" "}
                    R$ {Number(service.price).toFixed(2)}
                  </p>
                </div>

                <div className="mt-5 flex gap-3">
                  <button className="flex-1 bg-zinc-800 hover:bg-zinc-700 text-white py-2 rounded transition">
                    Editar
                  </button>

                  <button className="flex-1 bg-red-600 hover:bg-red-700 text-white py-2 rounded transition">
                    Excluir
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}