import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/auth/Login";
import Register from "./pages/auth/Register";
import PrivateRoute from "./routes/PrivateRoute";
import Unauthorized from "./pages/auth/Unauthorized";
import CreateService from "./pages/admin/CreateService";
import ServicesList from "./pages/admin/ServicesList";
import AdminDashboard from "./pages/admin/AdminDashboard";
import UsersList from "./pages/admin/UsersList";

// páginas exemplo
function Dashboard() {
  return <h1 className="text-white">Cliente</h1>;
}

function Admin() {
  return <h1 className="text-white">Admin</h1>;
}

function Barber() {
  return <h1 className="text-white">Barber</h1>;
}

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* públicas */}
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/unauthorized" element={<Unauthorized />} />

        {/* cliente */}
        <Route
          path="/dashboard"
          element={
            <PrivateRoute allowedRoles={["Client"]}>
              <Dashboard />
            </PrivateRoute>
          }
        />

        {/* admin */}
        <Route
          path="/admin"
          element={
            <PrivateRoute allowedRoles={["Admin"]}>
              <AdminDashboard />
            </PrivateRoute>
          }
        />

        {/* barber */}
        <Route
          path="/barber"
          element={
            <PrivateRoute allowedRoles={["Professional"]}>
              <Barber />
            </PrivateRoute>
          }
        />

      <Route
        path="/admin/services/create"
        element={
          <PrivateRoute allowedRoles={["Admin"]}>
            <CreateService />
          </PrivateRoute>
        }
      />

      <Route
        path="/admin/services"
        element={
          <PrivateRoute allowedRoles={["Admin"]}>
            <ServicesList />
          </PrivateRoute>
      }
      />
      <Route
        path="/admin/users"
        element={
          <PrivateRoute allowedRoles={["Admin"]}>
            <UsersList />
          </PrivateRoute>
        }
      />
    </Routes>
    </BrowserRouter>
  );
}

export default App;