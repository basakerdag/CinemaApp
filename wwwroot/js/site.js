
document.addEventListener('DOMContentLoaded', function () {
    const nav = document.getElementById('mainNav');
    if (nav) {
        window.addEventListener('scroll', () => {
            if (window.scrollY > 60) {
                nav.style.background = 'rgba(10,10,15,0.98)';
                nav.style.boxShadow = '0 4px 30px rgba(0,0,0,0.5)';
            } else {
                nav.style.background = 'rgba(10,10,15,0.95)';
                nav.style.boxShadow = 'none';
            }
        });
    }

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, { threshold: 0.1 });

    document.querySelectorAll('.movie-card, .genre-card, .stat-card').forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        el.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
        observer.observe(el);
    });

    const ratingInput = document.getElementById('ratingInput');
    const ratingDisplay = document.getElementById('ratingDisplay');
    if (ratingInput && ratingDisplay) {
        ratingInput.addEventListener('input', function () {
            ratingDisplay.textContent = this.value + '/10';
        });
    }

    const posterInput = document.getElementById('PosterUrl');
    const posterPreview = document.getElementById('posterPreview');
    if (posterInput && posterPreview) {
        posterInput.addEventListener('input', function () {
            if (this.value) {
                posterPreview.src = this.value;
                posterPreview.style.display = 'block';
            } else {
                posterPreview.style.display = 'none';
            }
        });
        if (posterInput.value) {
            posterPreview.src = posterInput.value;
            posterPreview.style.display = 'block';
        }
    }

    document.querySelectorAll('.admin-menu-item[data-tab]').forEach(btn => {
        btn.addEventListener('click', function () {
            const tab = this.dataset.tab;
            document.querySelectorAll('.admin-menu-item').forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            document.querySelectorAll('.admin-tab-panel').forEach(p => {
                p.style.display = p.dataset.panel === tab ? 'block' : 'none';
            });
        });
    });

    setTimeout(() => {
        document.querySelectorAll('.alert').forEach(a => {
            const bsAlert = new bootstrap.Alert(a);
            bsAlert.close();
        });
    }, 4000);
});
