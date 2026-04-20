import { useState } from "react";
import { login } from "../../services/auth.service";
import { getUser } from "../../utils/auth";


export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");


  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await login({ email, password });
      console.log("TOKEN:", response.data.token);
      localStorage.setItem("token", response.data.token);

      const user = getUser();
      console.log("USER DECODED:", user);
      alert("Login realizado!");
      if (user.role === "Admin") {
      window.location.href = "/admin";
      } else if (user.role === "professional") {
      window.location.href = "/barber";
      } else {
      window.location.href = "/dashboard";
      }
    } catch (error) {
      alert("Email ou senha inválidos");
    }
  };

  return (
    
    <div
      className="min-h-screen flex items-center justify-center bg-black bg-cover bg-center relative"
      style={{
        backgroundImage: "url('/barber-bg.png')",
      }}
    >
      {/* Overlay escuro */}
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
          Barber<span className="text-yellow-500">Flow</span>
        </h1>

        {/* Form */}
        <form onSubmit={handleLogin} className="flex flex-col gap-4">
          
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

          <button
            type="submit"
            className="bg-yellow-500 hover:bg-yellow-600 text-black font-bold py-3 rounded transition-all duration-200 hover:scale-[1.02]"
          >
            Entrar
          </button>

        </form>
        <br></br>
        <p className="text-sm text-zinc-400 mt-4 text-center">
          Não tem conta?{" "}
          <a href="/Register" className="text-yellow-500 hover:underline">
            Registrar
          </a>
        </p>

      </div>
    </div>
  );
}