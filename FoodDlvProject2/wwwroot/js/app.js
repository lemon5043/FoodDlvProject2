for (let i = 1; i <= 20; i++) {
    const row = document.querySelector(`#row-${i}`);
    if (row == null) continue;

    row.addEventListener("click", () => {
        window.location.href = `/Staffs/Details/${i}`;
    });
}