export default function AdminDashboard() {
  return (
    <div
      className="min-h-screen bg-black bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/barber-bg.png')",
      }}
    >
      {/* overlay */}
      <div className="absolute inset-0 bg-black/85"></div>

      <div className="relative p-10">

        {/* HEADER */}
        <div className="text-center mb-12">
          
          <img
            src="/barber-bg.png"
            className="w-16 mx-auto mb-4"
            alt="logo"
          />

          <h1 className="text-4xl font-bold text-white">
            Painel <span className="text-yellow-500">Admin</span>
          </h1>

          <p className="text-zinc-400 mt-2">
            Gerencie usuários, serviços e configurações do sistema
          </p>
        </div>

        {/* CARDS */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 max-w-5xl mx-auto">

          {/* USERS */}
          <div
            onClick={() => (window.location.href = "/admin/users")}
            className="cursor-pointer bg-zinc-900/70 backdrop-blur p-6 rounded-2xl border border-zinc-700 hover:border-yellow-500 hover:scale-[1.03] transition-all"
          >
            <h2 className="text-xl font-bold mb-2 text-white">
              👤 Usuários
            </h2>
            <p className="text-zinc-400">
              Gerenciar permissões e acessos
            </p>
          </div>

          {/* CREATE SERVICE */}
          <div
            onClick={() => (window.location.href = "/admin/services/create")}
            className="cursor-pointer bg-zinc-900/70 backdrop-blur p-6 rounded-2xl border border-zinc-700 hover:border-yellow-500 hover:scale-[1.03] transition-all"
          >
            <h2 className="text-xl font-bold mb-2 text-white">
              ✂️ Criar Serviço
            </h2>
            <p className="text-zinc-400">
              Adicionar novos serviços ao sistema
            </p>
          </div>

          {/* LIST SERVICES */}
          <div
            onClick={() => (window.location.href = "/admin/services")}
            className="cursor-pointer bg-zinc-900/70 backdrop-blur p-6 rounded-2xl border border-zinc-700 hover:border-yellow-500 hover:scale-[1.03] transition-all"
          >
            <h2 className="text-xl font-bold mb-2 text-white">
              📋 Serviços
            </h2>
            <p className="text-zinc-400">
              Visualizar e gerenciar serviços
            </p>
          </div>

        </div>
      </div>
    </div>
  );
}