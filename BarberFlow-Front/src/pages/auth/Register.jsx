import { useState } from "react";
import { register } from "../../services/auth.service";

export default function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handleRegister = async (e) => {
    e.preventDefault();

    if (password !== confirmPassword) {
      alert("As senhas não coincidem");
      return;
    }

    try {
      await register({ email, password });

      alert("Conta criada com sucesso!");

      window.location.href = "/";
    } catch (error) {
      alert("Erro ao cadastrar");
    }
  };

  return (
    <div
      className="min-h-screen flex items-center justify-center bg-black bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/barber-bg.png')",
      }}
    >
      {/* Overlay */}
      <div className="absolute inset-0 bg-black/80"></div>

      {/* Card */}
      <div className="relative bg-zinc-900/80 p-8 rounded-2xl shadow-2xl border border-zinc-700 w-full max-w-sm backdrop-blur">
        
        {/* Logo */}
        <img
          src="/barber-bg.png"
          alt="logo"
          className="w-16 mx-auto mb-4"
        />

        {/* Título */}
        <h1 className="text-3xl font-bold text-white text-center mb-6 tracking-wide">
          Criar conta
        </h1>

        {/* Form */}
        <form onSubmit={handleRegister} className="flex flex-col gap-4">

          <input
            type="email"
            placeholder="Email"
            className="p-3 rounded bg-zinc-800 text-white outline-none focus:ring-2 focus:ring-yellow-500"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <input
            type="password"
            placeholder="Senha"
            className="p-3 rounded bg-zinc-800 text-white outline-none focus:ring-2 focus:ring-yellow-500"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <input
            type="password"
            placeholder="Confirmar senha"
            className="p-3 rounded bg-zinc-800 text-white outline-none focus:ring-2 focus:ring-yellow-500"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />

          <button
            type="submit"
            className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-3 rounded transition-all duration-200 hover:scale-[1.02]"
          >
            Criar conta
          </button>

        </form>
        <br></br>
        {/* Link login */}
        <p className="text-sm text-zinc-400 mt-4 text-center">
          Já tem conta?{" "}
          <a href="/" className="text-yellow-500 hover:underline">
            Entrar
          </a>
        </p>

      </div>
    </div>
  );
}