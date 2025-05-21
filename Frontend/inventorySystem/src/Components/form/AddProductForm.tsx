import { useState } from "react";
import { useNavigate } from "react-router-dom";

const AddProductForm = () => {
  const [productName, setProductName] = useState('');
  const [productCategory, setProductCategory] = useState('');
  const [loading, setLoading] = useState(false);
  
  const navigate = useNavigate()

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await fetch('http://localhost:9000/v1/products', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          productName: productName,
          productCategory: productCategory,
        }),
      });

      if (!response.ok) {
        throw new Error(`Erro ao criar produto: ${response.statusText}`);
      }

      const data = await response.json();
      alert(`Produto criado com sucesso! Produto: ${data.productName}`);
      navigate('/products');
      setProductName('');
      setProductCategory('');
    } catch (error) {
    } finally {
      setLoading(false);
    }
  };

  return (
    <main>
      <h1>Cadastrar Produto</h1>
      <form id="productForm" onSubmit={handleSubmit}>
        <label htmlFor="productName">Nome do Produto:</label>
        <input
          type="text"
          id="productName"
          name="productName"
          placeholder="Digite o nome do produto"
          value={productName}
          onChange={(e) => setProductName(e.target.value)}
          required
        />

        <label htmlFor="productCategory">Categoria:</label>
        <input
          type="text"
          id="productCategory"
          name="productCategory"
          placeholder="Digite a categoria do produto"
          value={productCategory}
          onChange={(e) => setProductCategory(e.target.value)}
          required
        />

        <button type="submit" disabled={loading}>
          {loading ? 'Enviando...' : 'Criar Produto'}
        </button>
      </form>

    </main>
  );
};

export default AddProductForm;
