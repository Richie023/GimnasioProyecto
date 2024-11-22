// Esperar a que el DOM esté listo
document.addEventListener("DOMContentLoaded", () => {
    const calculateButton = document.getElementById("calculate-btn");

    calculateButton.addEventListener("click", () => {
        const weight = parseFloat(document.getElementById("weight").value);
        const height = parseFloat(document.getElementById("height").value);
        const age = parseInt(document.getElementById("age").value);
        const gender = document.getElementById("gender").value;

        const resultDiv = document.getElementById("bmi-result");

        // Validar entradas
        if (isNaN(weight) || isNaN(height) || isNaN(age) || !gender) {
            resultDiv.innerHTML = "<p class='text-danger'>Por favor, completa todos los campos correctamente.</p>";
            return;
        }

        if (weight <= 0 || height <= 0 || age <= 0) {
            resultDiv.innerHTML = "<p class='text-danger'>Peso, altura y edad deben ser mayores a cero.</p>";
            return;
        }

        // Calcular IMC
        const heightInMeters = height / 100;
        const bmi = (weight / (heightInMeters ** 2)).toFixed(2);

        // Categorizar el IMC
        let category = "";
        if (bmi < 18.5) {
            category = "Bajo peso";
        } else if (bmi >= 18.5 && bmi < 24.9) {
            category = "Peso normal";
        } else if (bmi >= 25 && bmi < 29.9) {
            category = "Sobrepeso";
        } else {
            category = "Obesidad";
        }

        // Mostrar resultado
        resultDiv.innerHTML = `
            <h3>Tu IMC es: <span class="text-primary">${bmi}</span></h3>
            <p>Categoría: <span class="text-warning">${category}</span></p>
        `;
    });
});
