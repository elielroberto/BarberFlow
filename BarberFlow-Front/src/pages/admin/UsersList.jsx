import { useEffect, useState } from "react";
import { getUsers, updateUserRole } from "../../services/user.service";

export default function UsersList() {
  const [users, setUsers] = useState([]);

  const loadUsers = async () => {
    try {
      const response = await getUsers();
      setUsers(response.data);
    } catch {
      alert("Erro ao carregar usuários");
    }
  };

  useEffect(() => {
    loadUsers();
  }, []);

  const handleRoleChange = async (userId, newRole) => {
  if (!confirm("Tem certeza que deseja alterar a role?")) return;

  try {
    await updateUserRole(userId, newRole);
    console.log(response.data);
    setUsers((prev) =>
      prev.map((u) =>
        u.id === userId ? { ...u, role: Number(newRole) } : u
      )
    );
  } catch {
    alert("Erro ao atualizar role");
  }
};

  return (
    <div className="min-h-screen bg-black text-white p-8">
      <button
          onClick={() => window.history.back()}
          className="text-yellow-500 mb-6 hover:underline flex items-center gap-2"
        >
          ← Voltar
        </button>  
      <h1 className="text-3xl font-bold mb-6">
        Usuários <span className="text-yellow-500">do Sistema</span>
      </h1>

      <div className="grid gap-4">

        {users.map((user) => (
          <div
            key={user.id}
            className="bg-zinc-900 p-4 rounded-xl border border-zinc-700 flex justify-between items-center"
          >
            <div>
              <p className="font-bold">{user.email}</p>
            </div>

            <select
              value={user.role}
              onChange={(e) =>
                handleRoleChange(user.id, e.target.value)
              }
              className="bg-zinc-800 text-white p-2 rounded border border-zinc-600"
            >
              <option value={0}>Client</option>
              <option value={1}>Professional</option>
              <option value={2}>Admin</option>
            </select>
          </div>
        ))}

      </div>
    </div>
  );
}