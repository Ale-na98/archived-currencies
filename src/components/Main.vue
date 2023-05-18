<template>
    <main class="main">
        <header class="header">
            <div class="container">
                <div class="row">
                    <h1 class="page-title">Archived exchange rates</h1>
                    <p class="site-disclosure">
                        On this site you can find archived exchange rates provided by
                        <a href="https://nbp.pl/statystyka-i-sprawozdawczosc/kursy/archiwum-kursow-srednich-tabela-b/">
                            Narodowy Bank Polski
                        </a>, which are not available in the
                        <a href="https://www.podatki.gov.pl/kalkulatory-podatkowe/kalkulator-walut/">
                            official currency calculator
                        </a>
                        on podatki.gov.pl.
                    </p>
                </div>
            </div>
        </header>
        <section class="section">
            <div class="container disclaimer-container py-3">
                <div class="card disclaimer-card">
                    <div class="card-body p-0">
                        <header class="d-flex disclaimer-header">
                            <span>DISCLAIMER</span>
                            <div type="button" data-bs-toggle="collapse" data-bs-target="#collapseExample"
                                aria-expanded="true" aria-controls="collapseExample"
                                @click="showDisclaimer = !showDisclaimer">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                    class="bi bi-caret-up-fill" viewBox="0 0 16 16" v-if="showDisclaimer">
                                    <path
                                        d="m7.247 4.86-4.796 5.481c-.566.647-.106 1.659.753 1.659h9.592a1 1 0 0 0 .753-1.659l-4.796-5.48a1 1 0 0 0-1.506 0z" />
                                </svg>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                    class="bi bi-caret-down-fill" viewBox="0 0 16 16" v-else>
                                    <path
                                        d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                                </svg>
                            </div>
                        </header>
                        <div class="content-disclaimer collapse show" id="collapseExample">
                            <p class="my-0">
                                Disclaimer body
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container py-3">
                <div class="row">
                    <form class="search-data" method="POST" name="post_from_date">
                        <div class="row align-items-end">
                            <div class="col-lg-3 py-2">
                                <label for="currency-select"> Choose the currency:</label>
                                <select class="form-select" id="currency-select" name="currency-select">
                                    <option value="currency-1"> 1</option>
                                    <option value="currency-2"> 2</option>
                                </select>
                            </div>
                            <div class="col-lg-3 py-2">
                                <label for="date-select"> Enter the transaction date:</label>
                                <input class="form-control" type="date" />
                            </div>
                            <div class="col-lg-1 py-2 text-center">
                                <button class="btn btn-secondary" type="submit">Search</button>
                            </div>
                        </div>
                    </form>
                    <article class="my-3">
                        <div class="row align-items-center">
                            <div class="col-lg-3 py-2">
                                The nearest date preceding the transaction:
                            </div>
                            <div class="col-lg-1 py-2 ">
                                <strong>12.05.2022</strong>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-lg-3 py-2">
                                Currency exchange rate for 1 unit:
                            </div>
                            <div class="col-lg-1 py-2">
                                <strong>0,055 PLN</strong>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-lg-3 py-2">
                                Official archive on the entered date:
                            </div>
                            <div class="col-lg-1 py-2">
                                <a
                                    href="/">Link</a>
                            </div>
                        </div>
                    </article>
                </div>
            </div>
        </section>
    </main>
</template>

<script>
import 'bootstrap/js/dist/collapse'

export default {
    data() {
        return {
            showDisclaimer: true
        }
    }
}
</script>